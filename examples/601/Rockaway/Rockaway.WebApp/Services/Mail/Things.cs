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

public class MailBodyRenderer : IMailBodyRenderer {
	private readonly IRazorEngine razor;
	private readonly IMjmlRenderer mjml;
	private readonly string mjmlTemplateSource = ReadEmbeddedResource("OrderConfirmation.mjml");
	private readonly string textTemplateSource = ReadEmbeddedResource("OrderConfirmation.txt");

	private IRazorEngineCompiledTemplate html;
	private readonly IRazorEngineCompiledTemplate text;

	public MailBodyRenderer(IRazorEngine razor, IMjmlRenderer mjml) {
		this.razor = razor;
		this.mjml = mjml;
		var htmlTemplateSource = mjml.Render(mjmlTemplateSource).Html;
		html = razor.Compile(EscapeCssRulesSoRazorDoesNotChokeOnThem(htmlTemplateSource));
		text = razor.Compile(textTemplateSource);
	}

	private string EscapeCssRulesSoRazorDoesNotChokeOnThem(string razorSource) =>
		cssRules.Aggregate(razorSource, (current, rule) => current.Replace($"{rule}", $"@{rule}"));

	private static readonly string[] cssRules = [
		"@bottom-center",
		"@bottom-left",
		"@bottom-left-corner",
		"@bottom-right",
		"@bottom-right-corner",
		"@charset",
		"@counter-style",
		"@document",
		"@font-face",
		"@font-feature-values",
		"@import",
		"@left-bottom",
		"@left-middle",
		"@left-top",
		"@keyframes",
		"@media",
		"@namespace",
		"@page",
		"@right-bottom",
		"@right-middle",
		"@right-top",
		"@supports",
		"@top-center",
		"@top-left",
		"@top-left-corner",
		"@top-right",
		"@top-right-corner"
	];

	private static string ReadEmbeddedResource(string resourceFileName) {
		var assembly = Assembly.GetEntryAssembly()!;
		var qualifiedName = $"{assembly.GetName().Name}.Templates.Mail.{resourceFileName}";
		var stream = assembly.GetManifestResourceStream(qualifiedName)!;
		return new StreamReader(stream).ReadToEnd();
	}

#if DEBUG
	public string RenderHtmlBody(TicketOrderViewData data) {
		var htmlTemplateSource = mjml.Render(mjmlTemplateSource).Html;
		html = razor.Compile(EscapeCssRulesSoRazorDoesNotChokeOnThem(htmlTemplateSource));
		return html.Run(data);
	}
#else
	public string RenderHtmlBody(TicketOrderViewData data) => html.Run(data);
#endif
	public string RenderTextBody(TicketOrderViewData data) => text.Run(data);
}

public interface ISmtpRelay {
	Task<string> SendMailAsync(MimeMessage message);
}


public class SmtpSettings {
	public string Hostname { get; set; } = "localhost";
	public string Username { get; set; } = String.Empty;
	public string Password { get; set; } = String.Empty;
	public int Port { get; set; } = 25;
	public bool Authenticate => !(String.IsNullOrWhiteSpace(Username) && String.IsNullOrWhiteSpace(Password));
}

public class SmtpRelay(SmtpSettings settings) : ISmtpRelay {
	public async Task<string> SendMailAsync(MimeMessage mail) {
		using var smtp = new SmtpClient();
		await smtp.ConnectAsync(settings.Hostname, settings.Port, SecureSocketOptions.StartTls);
		if (settings.Authenticate) {
			await smtp.AuthenticateAsync(settings.Username, settings.Password);
		}
		var result = await smtp.SendAsync(mail);
		await smtp.DisconnectAsync(true);
		return result;
	}
}