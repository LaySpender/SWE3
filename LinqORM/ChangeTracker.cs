using System;
using System.Collections.Generic;
using System.Text;

namespace LinqORM
{
    internal class ChangeTracker
    {
        public List<object> AllObjects;
        public List<object> Modified;
        public List<object> ToSave;
        public List<object> ToDelete;

        public ChangeTracker()
        {
            AllObjects = new List<object>();
            Modified = new List<object>();
            ToSave = new List<object>();
            ToDelete = new List<object>();
        }
    }
}
