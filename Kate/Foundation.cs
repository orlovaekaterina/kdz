using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kate
{
    public class VIP
    {
        int balkon;

        public int Balkon
        {
            get { return balkon; }
            set { balkon = value; }
        }
        int drink;

        public int Drink
        {
            get { return drink; }
            set { drink = value; }
        }
        int pillow;

        public int Pillow
        {
            get { return pillow; }
            set { pillow = value; }
        }
        int count_b;
        public VIP(string balkon, string drink, int pillow, Person b)
        {
            if (balkon == "Да") Balkon = 1; else Balkon = 0;
            if (drink == "Да")
            {
                Drink = 1;
            }
            else Drink = 0;
            Pillow = pillow;
            count_b = Calculation(b);
        }

        public int Calculation(Person b)
        {
            int answer = b.Length / 3 * (10000 + Balkon * 200 + Drink * 300+pillow*1000);
            if (b.Length % 3 > 0) answer += (10000 + Balkon * 200 + Drink * 300 + pillow * 1000);

            return answer;
        }
        public string Show()
        {
            return String.Format("Тип объекта: {0} Стоимость: {1}","VIP   ",count_b);
        }

    }
}
