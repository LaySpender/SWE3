using LinqORM.Attributes;
using LinqORM.Mock;
using LinqORM.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqORM.Interfaces;
using System.Text;
using LinqORM.Exceptions;

namespace LinqORM
{
    public class ORM
    {
        private ChangeTracker tracker;

        public ORM()
        {
            tracker = new ChangeTracker();
        }

        private bool HasToSave { get; set; }

        public IQueryable<T> GetQuery<T>() 
        {
            if (HasToSave) throw new ActionsNotExecutedException();
            return new LinqQueryable<T>(this);
        }

        internal ISQLSelectProvider GetSelectProvider<T>(Expression expression)
        {
            return new MSSQLSelectProvider<T>(expression);
        }

        internal ISQLInsertProvider GetInsertProvider(object obj)
        {
            return new MSSQLInsertProvider(obj);
        }

        internal ISQLDeleteProvider GetDeleteProvider(object obj)
        {
            return new MSSQLDeleteProvider(obj);
        }

        internal ISQLUpdateProvider GetUpdateProvider(object obj)
        {
            return new MSSQLUpdateProvider(obj);
        }

        internal IEnumerable<T> MaterializeObjects<T>(IEnumerable<Dictionary<string, object>> resultSet)
        {
            List<T> result = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var row in resultSet)
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    foreach (var attribute in property.GetCustomAttributes())
                    {
                        if (attribute is ColumnAttribute)
                        {
                            ColumnAttribute colAttr = (ColumnAttribute)attribute;
                            property.SetValue(instance, row[colAttr.Name]);
                        }
                        else if (attribute is PrimaryKeyAttribute)
                        {
                            var pkatt = (PrimaryKeyAttribute)attribute;
                            property.SetValue(instance, row[pkatt.Name]);
                        }
                    }
                }
                instance = tracker.ReplaceTracked(instance);
                tracker.Track(instance);
                result.Add(instance);                
            }
            return result;
        }

        public void Attach<T>(T obj)
        {
            if (HasToSave) throw new ActionsNotExecutedException();
            HasToSave = true;
            tracker.AddToSave(obj);
        }

        //private void AddToSave<T>(T obj)
        //{
        //    tracker.ToSave.Add(obj);
        //    AddToTracking(obj);
        //}

        public void Delete<T>(T obj)
        {
            if (HasToSave) throw new ActionsNotExecutedException();
            HasToSave = true;
            tracker.AddToDelete(obj);
        }

        //private void AddToDelete<T>(T obj)
        //{
        //    tracker.ToDelete.Add(obj);
        //    tracker.AllObjects.Remove(obj);
        //}

        //public void Update<T>(T obj)
        //{
        //    if (HasToSave) throw new ActionsNotExecutedException();
        //    HasToSave = true;
        //    tracker.Modified.Add(obj);
        //}

        public void SaveChanges()
        {
            // todo: get all the information from changeTracker and update, delete and insert all the data
            SubmitDelete(tracker.ToDelete);
            SubmitInsert(tracker.ToSave);
            SubmitUpdate(tracker.Modified);
            HasToSave = false;
        }

        private void SubmitDelete(List<object> toDelete)
        {
            foreach (var entry in toDelete)
            {
                ISQLDeleteProvider sdp = GetDeleteProvider(entry);
                sdp.DeleteObject();
            }
            toDelete.RemoveRange(0, toDelete.Count);
        }

        private void SubmitInsert(List<object> toSave)
        {
            foreach (var entry in toSave)
            {
                ISQLInsertProvider sip = GetInsertProvider(entry);
                sip.InsertObject(entry);
            }
            toSave.RemoveRange(0, toSave.Count);
        }

        private void SubmitUpdate(List<object> modified)
        {
            foreach (var entry in modified)
            {
                ISQLUpdateProvider sup = GetUpdateProvider(entry);
                sup.UpdateObject(entry);
            }
            modified.RemoveRange(0, modified.Count);
        }
    }
}
