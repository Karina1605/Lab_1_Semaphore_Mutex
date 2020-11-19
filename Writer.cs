using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab_1
{
    /// <summary>
    /// Класс писателя, имеет свой поток и номер
    /// </summary>
    class Writer 
    {
        Random r = new Random();
        public event Action<string, int> Action;
        Thread t;
        Buffer s;
        int num;
        int countSentMess = 0;
        public Writer(int number, Buffer s)
        {
            this.s = s;
            this.num = number;
            t = new Thread(Run);
            t.IsBackground = true;
            t.Start();

        }
        //Метод отправыки сообщения
        public string WriteMessage()
        {
            //Формируем сообщение
            countSentMess++;
            string ms = "Сообщение "+countSentMess+" от писателя " + num;

            //если не было доставлено формируем сообщение об ошибке
            if (!s.WriteMessage(ms)) 
                ms+=" не было доставлено, так как буфер полон";
            return ms;
        }
        //Метод потока
        void Run()
        {
            while (true)
            {
                //через рандомный промежуток времени посылаем сообщения
                Thread.Sleep(r.Next(5000, 8000));
                string res = WriteMessage();
                //передаем результат действия на форму
                Action?.Invoke(res, s.CurrentCount);    
            }
        }
        
    }
}
