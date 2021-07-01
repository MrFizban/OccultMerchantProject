using System;

namespace warehouse.items
{
    public struct Dice
    {
        public int number { get; set; }
        public int value { get; set; }

        public Dice(int _number, int _value)
        {
            this.number = _number;
            this.value = _value;
        }

        public Dice(string str)
        {
            var tmp = str.Split('d');
            this = new Dice(Int32.Parse(tmp[0]),Int32.Parse(tmp[1]));
            
        }

        public override string ToString()
        {
            return $"{this.number.ToString()}d{this.value.ToString()}";
        }

      
        /// <summary>
        /// converte una stringa nell fomato XdX nei valori del dado
        /// </summary>
        /// <param name="str">stringa da convertire</param>
        /// <returns>il dato convertito</returns>
       
    }
}