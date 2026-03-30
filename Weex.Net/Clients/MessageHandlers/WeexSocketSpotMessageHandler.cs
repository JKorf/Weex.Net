using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Text.Json;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using Weex.Net.Objects.Models;
using System.Linq;

namespace Weex.Net.Clients.MessageHandlers
{
    internal class WeexSocketSpotMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = WeexExchange._serializerContext;

        public WeexSocketSpotMessageHandler()
        {
            AddTopicMapping<WeexSocketEvent<WeexKlineUpdate[]>>(x => x.Symbol + x.Data.First().IntervalString);
            AddTopicMapping<WeexSocketEvent>(x => x.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("e"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("e")!,
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("event"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("event")!,
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("type"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("type")!,
            },

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!,
            }
        ];
    }
}
