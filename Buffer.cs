using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab_1
{
    //Класс буфера
    public class Buffer
    {
        static Random r = new Random();
        //сам буфер
        Stack<string> Messages { get; set; }
        //Семафоры для читателей и писателей
        Semaphore readsemaphore = new Semaphore(1, 2);
        Semaphore writesemaphore = new Semaphore(2, MaxMessages);
        //Мьютекс, блокирубщий Messages
        Mutex mutex = new Mutex();
        //Макс кол-во сообщений в буфере
        public const int MaxMessages = 5;
        public int CurrentCount => Messages.Count;
        public Buffer()
        {
            Messages = new Stack<string>();
        }
        //Попытка записи нового сообщения
        public bool WriteMessage(string st)
        {
            bool res = false;
            //Ждем семафор писателей
            writesemaphore.WaitOne();
            //Ждем мьютекс
            mutex.WaitOne();
            //Пытаемся записать сообщение
            Messages.Push(st);
            res = true;
            //Освобождаем мьютекс
            mutex.ReleaseMutex();
            //Разрешаем чтение
            //if (r.Next(5)>2)
              //  writesemaphore.Release();
           // else
                readsemaphore.Release();
            
            return res;
        }
        //Чтение из буфера
        public string ReadMessage()
        {
            string result = null;
            //Ждем семафор для читателей
            readsemaphore.WaitOne();
            //Ждем мьютекс
            mutex.WaitOne();
            //Пытаемся прочитать сообщение
                result = Messages.Pop();
            //Освобождаем мьютекс
            mutex.ReleaseMutex();
            //Разрешаем запись
            //if (r.Next(5) > 2)
              //  readsemaphore.Release();
            //else
                writesemaphore.Release();
            return result;
        }
    }
}
