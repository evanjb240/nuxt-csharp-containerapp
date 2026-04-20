using SampleApp.Models;
using Microsoft.AspNetCore.Mvc;
using Resend;

namespace SampleApp;

[Route("api/[controller]")]
[ApiController]
public class ContactController(IResend client): ControllerBase {
    
    [HttpPost("SendMessage")]
public async Task<IActionResult> Contact(Models.Contact form)
{
    try
    {
        var response = await client.EmailSendAsync(new()
        {
            From = "no-reply@sample-app.com",
            To = new EmailAddressList(){"evanjb@att.net" , form.Email!},
            Subject = $"Contact Us: {form.Subject}",
            TextBody = $"{form.Name} has contacted us with the following message:\n\n{form.Message}"
        });
        Console.WriteLine(response.Success);
        
        return Ok("Email sent successfully");
    }
    catch (Exception ex)
    {
        return BadRequest($"Failed to send email: {ex.Message}");
    }
}
}