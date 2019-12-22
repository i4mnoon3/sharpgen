using System;
using System.Collections.Generic;
using SharpGenerator.Helpers;

namespace SharpGenerator.Models
{
    public class Database
    {
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        
        public Database(string name)
        {
            this.Name = name;
            Tables = new List<Table>();
        }
        
        public void AddTables(List<Table> tables)
        {
            foreach (var t in tables) {
                AddTable(t);
            }
        }
        
        public void AddTable(Table table)
        {
            table.Database = this;
            Tables.Add(table);
        }
    }
    
    public class Table
    {
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
        public List<Column> Keys { get; set; }
        public Database Database { get; set; }
        
        public Table() : this("")
        {
        }
        
        public Table(string name)
        {
            this.Name = name;
            Columns = new List<Column>();
            Keys = new List<Column>();
        }
        
        public void AddKey(string name, string type)
        {
            AddKey(new Column(name, type));
        }
        
        public void AddKey(Column key)
        {
            if (key != null) {
                Keys.Add(key);
            }
        }
        
        public void AddColumn(string name, string type, bool notNull, bool primaryKey, bool autoIncrement)
        {
            AddColumn(new Column(name, type, notNull, primaryKey, autoIncrement));
        }
        
        public void AddColumn(string name)
        {
            AddColumn(new Column(name));
        }
        
        public void AddColumn(Column column)
        {
            column.Table = this;
            this.Columns.Add(column);
        }
        
        public Class ToClass()
        {
            var c = new Class(Name);
            c.Namespace = Database.Name.ToPascalCase();
            foreach (var col in Columns) {
                c.AddProperty(new Property(col.Name, GetPropertyType(col.Type)));
            }
            return c;
        }
        
        string GetPropertyType(string type)
        {
            switch (type.ToLower()) {
                case "int":
                    case "integer": return "int";
                    case "double": return "double";
                    case "decimal": return "decimal";
                    case "float": return "float";
                case "date":
                case "datetime":
                    return "DateTime";
                    default: return "string";
            }
        }
    }
    
    public class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Table Table { get; set; }
        public bool NotNull { get; set; }
        public bool PrimaryKey { get; set; }
        public bool AutoIncrement { get; set; }
        
        public Column(string name) : this(name, "varchar(255)")
        {
        }
        
        public Column(string name, string type) : this(name, type, false, false, false)
        {
        }
        
        public Column(string name, string type, bool notNull, bool primaryKey, bool autoIncrement)
        {
            this.Name = name;
            this.Type = type;
            this.NotNull = notNull;
            this.PrimaryKey = primaryKey;
            this.AutoIncrement = autoIncrement;
        }
    }
}
