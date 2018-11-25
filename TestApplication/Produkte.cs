using LinqORM;
using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication
{
    public class Produkte : ObservableObject
    {
        private int id;
        private int werkId;
        private string bezeichnung;

        [PrimaryKey]
        public int Id { get => id; set => SetProperty(ref id, value); }
        [Column]
        public int WerkId { get => werkId; set => SetProperty(ref werkId, value); }
        [Column]
        public string Bezeichnung { get => bezeichnung; set => SetProperty(ref bezeichnung, value); }
    }
}
