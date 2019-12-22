using System;
using System.Collections.Generic;

namespace SharpGenerator.Models
{
    public class Class
    {
        public string Namespace { get; set; }
        public string Name { get; set; }
        public List<Property> Properties { get; set; }
        
        public Class() : this("")
        {
        }
        
        public Class(string name)
        {
            this.Name = name;
            this.Properties = new List<Property>();
        }
        
        public void AddProperty(string name)
        {
            AddProperty(name, "string");
        }
        
        public void AddProperty(string name, string type)
        {
            AddProperty(new Property(name, type));
        }
        
        public void AddProperty(Property property)
        {
            property.Class = this;
            Properties.Add(property);
        }
        
        public override string ToString()
        {
            string s = string.Format(@"using System;

namespace __NAMESPACE__.Models
{{
    public class __NAME__
    {{
__PROPERTIES__
        public __NAME__()
        {{
        }}
    }}
}}");
            s = s.Replace("__NAMESPACE__", Namespace);
            s = s.Replace("__NAME__", Name);
            string properties = "";
            foreach (var p in Properties) {
                properties += "        " + p.ToString() + Environment.NewLine;
            }
            s = s.Replace("__PROPERTIES__", properties);
            return s;
        }
    }
    
    public class Property
    {
        public string Type { get; set; }
        public Class Class { get; set; }
        string name;
        
        public string Name {
            get {
                string n = name == Class.Name ? "Name" : name;
                return n; 
            }
            set { name = value; }
        }
        
        public Property(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
        
        public override string ToString()
        {
            string s = string.Format(@"public {0} {1} {{ get; set; }}", Type, Name);
            return s;
        }

    }
}
