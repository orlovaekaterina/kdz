using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kate
{
   public class Client
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

        int count_b;
        public Client(Person b, string balkon, string drink)
        {
            if (balkon == "Да") Balkon = 1; else Balkon = 0;
            if (drink == "Да")
            {
                Drink = 1;
            }
            else Drink = 0;

            count_b = Calculation(b);
        }
        public int Calculation(Person b)
        {
            int answer = b.Length/3*(1000+Balkon*200+Drink*300);
            if (b.Length % 3 > 0) answer += (1000 + Balkon * 200 + Drink * 300);
           
            return answer;
        }

        public string Show()
        {
            return String.Format("Тип клиента:{0} Стоимость: {1}"," Клиент             ",count_b);
        }
    }

}
