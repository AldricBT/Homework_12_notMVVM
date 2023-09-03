﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    internal abstract class DataBase<T> : ICloneable
        where T : class
    {
        private readonly string _pathToData;
        private ObservableCollection<T> _dataList;

        public ObservableCollection<T> Data
        {
            get => _dataList;
        }

        /// <summary>
        /// Добавление данных
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            _dataList.Add(data);
            Save();
        }

        /// <summary>
        /// Удаление данных
        /// </summary>
        /// <param name="data">Экземпляр данных</param>
        public void Remove(T data)
        {
            if (data == null)
                return;
            _dataList.Remove(data);
            Save();
        }

        /// <summary>
        /// Получает уникальный Id новых данных
        /// </summary>
        /// <returns></returns>
        public abstract int GetNewId();

        public DataBase(string pathToData)
        {
            _pathToData = pathToData;
            _dataList = new ObservableCollection<T>();

            if (File.Exists(_pathToData))
                Load();            
        }

        /// <summary>
        /// Выгрузка базы клиентов из файла
        /// </summary>
        private void Load()
        {
            string jsonString = File.ReadAllText(_pathToData);
            if (jsonString != null)
                _dataList = JsonSerializer.Deserialize<ObservableCollection<T>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        /// <summary>
        /// Сохранение базы клиентов в файл
        /// </summary>
        public void Save()
        {
            string jsonString = JsonSerializer.Serialize(_dataList);
            File.WriteAllText(_pathToData, jsonString);
        }

        public abstract object Clone();
    }
}
