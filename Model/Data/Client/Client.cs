﻿using Homework_12_notMVVM.Model.Data.Account;
using Homework_12_notMVVM.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Homework_12_notMVVM.Model.Data
{
    public class Client : ViewModel, ICloneable //чтобы заново не реализовывать INPC
    {
        private int _id;
        private string _name;
        private ObservableCollection<AccountBase> _accounts;

        [JsonInclude]
        public int Id 
        {
            get => _id; 
            private set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        [JsonInclude]
        public ObservableCollection<AccountBase> Accounts
        {
            get => _accounts;
            private set => _accounts = value;
        }

        public Client(int id, string name)
        {         
            _id = id;
            _name = name;
            _accounts = new ObservableCollection<AccountBase>();
            //При добавлении клиента автоматически открывает ему расчётный счет
            OpenNewAccount(new AccountPayment(StaticMainData.Accounts.GetNewId(),
                AccountBase.CurrencyEnum.RUR, _id));
        }

        [JsonConstructor]
        public Client(int id, string name, ObservableCollection<AccountBase> accounts)
        {
            _id = id;
            _name = name;
            _accounts = accounts;
        }

        /// <summary>
        /// Открытие счёта
        /// </summary>
        public void OpenNewAccount(AccountBase account)
        {
            StaticMainData.Accounts.Add(account);
            //добавление ссылки на счет клиента в данные клиента
            _accounts =  StaticMainData.Accounts;
            
        }

        public void RemoveAccount(AccountBase account)
        {
            StaticMainData.Accounts.Remove(account);
            _accounts.Remove(account);
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, numofacc: {Accounts.Count} ";
        }

        public object Clone()
        {
            return new Client(this._id, this._name, this._accounts);
        }
    }
}
