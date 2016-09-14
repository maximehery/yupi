﻿/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Reflection;

namespace Yupi.Net.DotNettyImpl
{
    public class MessageHandler<T> : ChannelHandlerAdapter, ISession<T>
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IChannel Channel;
        private readonly ConnectionClosed<T> OnConnectionClosed;
        private readonly ConnectionOpened<T> OnConnectionOpened;
        private readonly MessageReceived<T> OnMessage;

        public MessageHandler(IChannel channel, MessageReceived<T> onMessage, ConnectionClosed<T> onConnectionClosed,
            ConnectionOpened<T> onConnectionOpened)
        {
            Channel = channel;
            OnMessage = onMessage;
            OnConnectionClosed = onConnectionClosed;
            OnConnectionOpened = onConnectionOpened;
        }

        public IPAddress RemoteAddress
        {
            get { return ((IPEndPoint) Channel.RemoteAddress).Address; }
        }

        public T UserData { get; set; }

        public void Disconnect()
        {
            Channel.DisconnectAsync();
        }

        public void Send(byte[] data)
        {
            Channel.WriteAndFlushAsync(data);
        }

        public void Send(ArraySegment<byte> data)
        {
            var buffer = new byte[data.Count];
            Array.Copy(data.Array, data.Offset, buffer, 0, data.Count);
            Send(buffer);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            IByteBuffer dataBuffer = message as IByteBuffer;

            var data = new byte[dataBuffer.ReadableBytes];

            dataBuffer.ReadBytes(data);

            OnMessage(this, data);

            dataBuffer.Release();
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Logger.Warn("A networking error occured", exception);
            context.CloseAsync();
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            OnConnectionOpened(this);
            base.ChannelActive(context);
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            OnConnectionClosed(this);
            base.ChannelInactive(context);
        }
    }
}