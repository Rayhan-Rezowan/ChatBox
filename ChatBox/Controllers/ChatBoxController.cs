using ChatBox.Data;
using ChatBox.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatBox.Controllers
{
    public class ChatBoxController : Controller
    {
        //
        // GET: /ChatBox/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllMessages()
        {
            ChatMessageDataAccess chatMessageDataAccess = new ChatMessageDataAccess();

            DataTable dtMessages = chatMessageDataAccess.GetAllMessage();

            List<ChatMessage> chatMessageList = new List<ChatMessage>();

            if (dtMessages != null && dtMessages.Rows.Count > 0)
            {
                foreach (DataRow drMessage in dtMessages.Rows)
                {
                    chatMessageList.Add(new ChatMessage
                        {
                            UserId = Convert.ToInt32(drMessage["UserId"]),
                            ChatMessageId = Convert.ToInt32(drMessage["ChatMessageId"]),
                            UserName = drMessage["UserName"].ToString(),
                            Message = drMessage["Message"].ToString(),
                            SendMessageTime = Convert.ToDateTime(drMessage["SendMessageTime"])
                        });
                }
            }
           
            return Json(new { ChatMessages = chatMessageList }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertUser(string userName)
        {
            ChatMessageDataAccess chatMessageDataAccess = new ChatMessageDataAccess();

            var result = chatMessageDataAccess.InsertUser(userName);

            return Json(new { userId = result });
        }

        [HttpPost]
        public JsonResult InsertMessage(int userId, string message)
        {
            ChatMessageDataAccess chatMessageDataAccess = new ChatMessageDataAccess();

            var result = chatMessageDataAccess.InsertMessage(userId, message);

            return Json(new { result = result });
        }

    }
}
