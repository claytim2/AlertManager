using Model.AbsModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DataServices
{
    public static class MessageService
    {
        /// <summary>
        /// Exibe uma mensagem
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="text">Texto da mensagem</param>
        /// <param name="title">Título da mensagem</param>
        /// <param name="type">Tipo de mensagem</param>
        public static void ShowMessage(this ControllerBase controller, string text, string title = "", MessageModel.EMessageType type = MessageModel.EMessageType.Information)
        {
            var messages = controller.TempData["Messages"] as List<MessageModel> ?? new List<MessageModel>();
            messages.Add(new MessageModel
            {
                Text = text,
                Title = title,
                Type = type
            });
            controller.TempData["Messages"] = messages;
        }

        /// <summary>
        /// Retorna uma string com o tipo de mensagem, basedo no enumerador
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MessageType(MessageModel.EMessageType type)
        {
            var retorno = "info";

            switch (type)
            {
                case MessageModel.EMessageType.Alert:
                    retorno = "warning";
                    break;
                case MessageModel.EMessageType.Error:
                    retorno = "error";
                    break;
                case MessageModel.EMessageType.Information:
                    retorno = "info";
                    break;
                case MessageModel.EMessageType.Success:
                    retorno = "success";
                    break;
            }
            return retorno;
        }
    }
}
