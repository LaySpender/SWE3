﻿using LinqORM.Attributes;
using LinqORM.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LinqORM
{
    internal class ChangeTracker
    {
        private Delegate _objectModifiedDelegate;
        public List<object> AllObjects;
        public List<object> Modified;
        public List<object> ToSave;
        public List<object> ToDelete;

        internal ChangeTracker()
        {
            _objectModifiedDelegate = Delegate.CreateDelegate(typeof(PropertyChangedEventHandler), this, "PropertyChanged");
            AllObjects = new List<object>();
            Modified = new List<object>();
            ToSave = new List<object>();
            ToDelete = new List<object>();
        }

        internal void Track(object obj)
        {
            if (AllObjects.Contains(obj)) return;
            AllObjects.Add(obj);
            obj.GetType().GetEvent("PropertyChanged").AddEventHandler(obj, _objectModifiedDelegate);
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (AllObjects.Contains(sender))
            {
                if (!Modified.Contains(sender)) Modified.Add(sender);
            }
            else
            {
                throw new Exception("shit is going down.");
            }
        }

        internal T ReplaceTracked<T>(T obj)
        {
            PropertyInfo idProperty = typeof(T).GetProperties().Single(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(PrimaryKeyAttribute)));
            var instance = AllObjects.OfType<T>().SingleOrDefault(x => (int)idProperty.GetValue(x) == (int)idProperty.GetValue(obj));
            return (instance != null) ? instance : obj;
        }

        internal void AddToSave(object obj)
        {
            if (AllObjects.Contains(obj) && !ToDelete.Contains(obj)) throw new AlreadyAddedException();
            if (ToDelete.Contains(obj))
            {
                ToDelete.Remove(obj);
                Modified.Add(obj);
                obj.GetType().GetEvent("PropertyChanged").AddEventHandler(obj, _objectModifiedDelegate);
            }
            else
            {
                AllObjects.Add(obj);
                ToSave.Add(obj);
            }  
        }

        internal void InstancesSaved()
        {
            foreach(var obj in ToSave)
            {
                obj.GetType().GetEvent("PropertyChanged").AddEventHandler(obj, _objectModifiedDelegate);
            }
            ToSave.Clear();
        }

        internal void InstancesDeleted()
        {
            foreach (var obj in ToDelete)
            {
                AllObjects.Remove(obj);
            }
            ToDelete.Clear();
        }

        internal void InstancesUpdated()
        {
            Modified.Clear();
        }

        internal void AddToDelete(object obj)
        {
            if (!AllObjects.Contains(obj)) throw new AlreadyAddedException();
            if (ToDelete.Contains(obj)) throw new Exception();
            if(ToSave.Contains(obj))
            {
                ToSave.Remove(obj);
            }
            if(Modified.Contains(obj))
            {
                Modified.Remove(obj);
            }
            ToDelete.Add(obj);
            obj.GetType().GetEvent("PropertyChanged").RemoveEventHandler(obj, _objectModifiedDelegate);
        }
    }
}
