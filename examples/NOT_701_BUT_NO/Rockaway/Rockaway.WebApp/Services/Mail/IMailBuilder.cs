using MimeKit;
using Rockaway.WebApp.Models;

namespace Rockaway.WebApp.Services.Mail;

public interface IMailBuilder {
	public MimeMessage BuildOrderConfirmationMail(TicketOrderMailData data);
}
