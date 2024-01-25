using System.Reflection;
using MailKit.Net.Smtp;
using MailKit.Security;
using Mjml.Net;
using RazorEngineCore;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Services.Mail;

public interface IMailBodyRenderer {
	string RenderHtmlBody(TicketOrderViewData data);
	string RenderTextBody(TicketOrderViewData data);
}
