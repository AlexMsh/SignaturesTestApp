using AutoMapper;
using Signant.Common.Models;
using SignantRecipient = Signant.Common.Models.Recipient;
using Signant.Global.Common.Enums;
using Signarutes.Domain.Contracts.models.Request;

namespace Signatures.Signant.Client.Proxy.Mappings
{
    public class SignatureRequestSignantProfile : Profile
    {
        public SignatureRequestSignantProfile()
        {
            // this mapping is not perfect (as I'm not reusing more granular ones, doing all the job in one place
            // but I didn't want to spend time on making it pretty
            // added automapper just to move this boilerplace into a single place and highlight the general concept
            CreateMap<SignatureRequest, Posting>()
                .ForPath(dest => dest.Recipients, op => op.MapFrom(src => new List<SignantRecipient>() {
                    new SignantRecipient()
                    {
                        Email =  src.Recipient.Email,
                        Name =  src.Recipient.Name,
                        NotifyByEmail = true,
                        Priority = 0
                    }
                }))
                .ForPath(
                    dest => dest.Attachments, op => op.MapFrom(src => new List<Attachment> {
                        new Attachment
                        {
                            ActionType = ActionType.Sign,
                            File = src.File.Content,
                            FileName = $"{Guid.NewGuid()}_{src.File.Name}",
                        }
                }))
                .ForMember(dest => dest.Title, op => op.MapFrom(src => src.Message))
                .ForMember(dest => dest.UseWidget, op => op.MapFrom(src => true))
                .ForMember(dest => dest.AutoActivate, op => op.MapFrom(src => true));
        }
    }
}
