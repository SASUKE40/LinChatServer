namespace LinChatServer.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Web;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    using LinChatServer.Model;

    using SuperWebSocket;

    public class MainViewModel : ViewModelBase
    {

        private ChatServer websocketServer = null;
        public MainViewModel()
        {
            websocketServer = new ChatServer();
            if (!websocketServer.Setup(Address, Port))
            {
                Console.WriteLine("LinChatWebSocket 设置WebSocket服务侦听地址失败");
                //                return;
            }
            ListenCommand = new RelayCommand(() => this.Start());
            StopCommand = new RelayCommand(() => this.Stop());
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            if (!websocketServer.Start())
            {
                Console.WriteLine("LinChatWebSocket 启动WebSocket服务侦听失败");
                return;
            }
            Console.WriteLine("LinChatWebSocket 启动服务成功");
        }

        /// <summary>
        /// 停止侦听服务
        /// </summary>
        public void Stop()
        {

            if (websocketServer != null)
            {
                websocketServer.Stop();
                Console.WriteLine("LinChatWebSocket 停止服务成功");
            }
        }

        /// <summary>
        /// The <see cref="Address" /> property's name.
        /// </summary>
        public const string AddressPropertyName = "Address";

        private string _address = "127.0.0.1";
        //        private string _address = "192.168.191.1";
        //                private string _address = "192.168.191.6";
        /// <summary>
        /// Sets and gets the Address property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                if (_address == value)
                {
                    return;
                }

                _address = value;
                RaisePropertyChanged(AddressPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Port" /> property's name.
        /// </summary>
        public const string PortPropertyName = "Port";

        private int _port = 2015;

        /// <summary>
        /// Sets and gets the Port property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Port
        {
            get
            {
                return _port;}

            set
            {
                if (_port == value)
                {
                    return;
                }

                _port = value;
                RaisePropertyChanged(PortPropertyName);
            }
        }

        public RelayCommand ListenCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
    }
}
