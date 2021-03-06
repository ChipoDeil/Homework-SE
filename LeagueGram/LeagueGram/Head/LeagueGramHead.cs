﻿using System;
using System.Collections.Generic;
using System.Text;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("PrivateChatTester")]
namespace LeagueGram
{
    internal class LeagueGramHead
    {
        public ChatFactory chatFactory;
        public IUserManager userManager;

        public Guid RegisterUser(string nickName)
        {
            return userManager.RegisterUser(nickName);
        }

        public void SendMessage(Guid userId, Guid chatId, string message)
        {
            chatFactory.GetChatById(chatId).SendMessage(userManager.GetUserById(userId), message);
        }

        public void DeleteMessage(Guid userId, Guid chatId, Guid messageId)
        {
            IChat currentChat = chatFactory.GetChatById(chatId);
            currentChat.DeleteMessage(userManager.GetUserById(userId), currentChat.FindMessageById(messageId));
        }

        public void EditMessage(Guid userId, Guid chatId, Guid messageId, string newMessage)
        {
            IChat currentChat = chatFactory.GetChatById(chatId);
            currentChat.EditMessage(userManager.GetUserById(userId), currentChat.FindMessageById(messageId),
                                                                                    newMessage);
        }

        public Guid CreatePrivateChat(Guid userId, Guid invitedPersonId)
        {
            IChat createdChat = chatFactory.CreatePrivateChat(userManager.GetUserById(userId),
                                                            userManager.GetUserById(invitedPersonId));
            userManager.JoinChat(userId, createdChat);
            userManager.JoinChat(invitedPersonId, createdChat);
            return createdChat.Id;
        }

        public Guid CreateGroup(Guid userId)
        {
            IChat createdChat = chatFactory.CreateGroup(userManager.GetUserById(userId));
            userManager.JoinChat(userId, createdChat);
            return createdChat.Id;
        }

        public Guid CreateChannel(Guid userId)
        {
            IChat createdChat = chatFactory.CreateChannel(userManager.GetUserById(userId));
            userManager.JoinChat(userId, createdChat);
            return createdChat.Id;
        }

        public void InviteUserToChat(Guid inviterId, Guid invitedPersonId, Guid chatId)
        {
            IChat currentChat = chatFactory.GetChatById(chatId);
            currentChat.InviteUser(userManager.GetUserById(inviterId),
                                                    userManager.GetUserById(invitedPersonId));
            userManager.JoinChat(invitedPersonId, currentChat);
        }

        public void EditRoleOfChatMember(Guid changer, Guid target, Guid chatId, ChatRole newRole)
        {
            chatFactory.GetChatById(chatId).EditRoleOfChatMember(userManager.GetUserById(changer), 
                                                            userManager.GetUserById(target), newRole);
        }

        public IEnumerable<IChat> GetChatsForUser(Guid userId)
        {
            IEnumerable<IChat> chats = new List<IChat>(userManager.GetUserById(userId).Chats);
            return chats;
        }

        public IEnumerable<IMessage> GetMessagesForUser(Guid userId, Guid chatId) {
            IEnumerable<IMessage> messages = new List<IMessage>(chatFactory.GetChatById(chatId)
                                                .GetMessages(userManager.GetUserById(userId)));
            return messages;
        }

        public LeagueGramHead() {
            chatFactory = new ChatFactory();
            userManager = new UserManager();
        }
    }
}
