using LinqORM;
using LinqORM.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApplication
{
    public class Werke : ObservableObject
    {
        private int id;
        private string bezeichnung;
        private string ort;
        private string strasse;

        [PrimaryKey]
        public int Id { get => id; set => SetProperty(ref id, value); }
        [Column]
        public string Bezeichnung { get => bezeichnung; set => SetProperty(ref bezeichnung, value); }
        [Column]
        public string Ort { get => ort; set => SetProperty(ref ort, value); }
        [Column]
        public string Strasse { get => strasse; set => SetProperty(ref strasse, value); }
    }
}
