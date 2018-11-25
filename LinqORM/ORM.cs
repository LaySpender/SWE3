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

            foreach (var column in resultSet)
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var property in properties)
                {
                    if (property.CustomAttributes.Any())
                    {
                        foreach (var attribute in property.GetCustomAttributes())
                        {
                            if (attribute is ColumnAttribute)
                            {
                                ColumnAttribute colAttr = (ColumnAttribute)attribute;
                                property.SetValue(instance, column[colAttr.Name]);
                            }
                        }
                    }
                }
                if (InstanceIsTracked(instance))
                {
                    result.Add(GetInstanceFromTracker(instance));
                }
                else
                {
                    AddToTracking(instance);
                    result.Add(GetInstanceFromTracker(instance));
                }
            }
            return result;
        }

        private bool InstanceIsTracked<T>(T obj)
        {
            PropertyInfo idProperty = typeof(T).GetProperties().Single(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(PrimaryKeyAttribute)));
            if (!tracker.AllObjects.OfType<T>().Any(x => (int)idProperty.GetValue(x) == (int)idProperty.GetValue(obj)))
            {                
                return false;
            }
            return true;
        }

        private T GetInstanceFromTracker<T>(T obj)
        {
            PropertyInfo idProperty = typeof(T)
                .GetProperties()
                .Single(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(PrimaryKeyAttribute)));
            if (!InstanceIsTracked<T>(obj))
            {
                throw new InstanceNotFoundException();
            }
            return tracker.AllObjects.OfType<T>().Single(x => (int)idProperty.GetValue(x) == (int)idProperty.GetValue(obj));
        }

        public void Attach<T>(T obj)
        {
            HasToSave = true;
            AddToSave(obj);
        }

        private void AddToSave<T>(T obj)
        {
            tracker.ToSave.Add(obj);
            AddToTracking(obj);
        }

        private void AddToTracking<T>(T obj)
        {
            tracker.AllObjects.Add(obj);
        }

        public void Delete<T>(T obj)
        {
            HasToSave = true;
            AddToDelete(obj);
        }

        private void AddToDelete<T>(T obj)
        {
            tracker.ToDelete.Add(obj);
            tracker.AllObjects.Remove(obj);
        }

        public void SaveChanges<T>()
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
        }

        private void SubmitInsert(List<object> toSave)
        {
            foreach (var entry in toSave)
            {
                ISQLInsertProvider sip = GetInsertProvider(entry);
                sip.InsertObject(entry);
            }
        }

        private void SubmitUpdate(List<object> modified)
        {
            foreach (var entry in modified)
            {
                ISQLUpdateProvider sup = GetUpdateProvider(entry);
                sup.UpdateObject(entry);
            }
        }
    }
}
