using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab_1
{
    /// <summary>
    /// Класс читателя, имеет свой поток и номер
    /// </summary>
    class Reader
    {
        Random r = new Random();
        public event Action<string, int> Action;
        Thread t;
        Buffer s;
        int num;
        public Reader(int number, Buffer s)
        {
            this.s = s;
            this.num = number;
            t = new Thread(Run);
            t.IsBackground = true;
            t.Start();
        }
        public string ReadMessage()
        {
            //Читаем сообщение
            string ms = s.ReadMessage();
            string res;
            //формируем отчет о прочтении
            if (ms!=null)
                res = "Читатель " + num + " прочитал сообщение \'"+ ms+"\'";
            else
                res = "Читатель " + num + ", буфер пуст ";
            return res;
        }
        void Run()
        {
            while (true)
            {
                //через рандомный промежуток времени читаем сообщения
                Thread.Sleep(r.Next(1000));
                string res = ReadMessage();
                //передаем результат действия на форму
                Action?.Invoke(res, s.CurrentCount);
            }
        }
    }
}
