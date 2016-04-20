using System;

namespace DateTimeWat
{
    public class SomeType
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }

        protected bool Equals(SomeType other)
        {
            return Date.Equals(other.Date) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SomeType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Date.GetHashCode()*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"Name: {Name}, Date: {Date}";
        }
    }
}