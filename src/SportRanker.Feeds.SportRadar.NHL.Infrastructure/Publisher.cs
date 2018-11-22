﻿using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SportRanker.Contracts.SystemEvents;
using SportRanker.Feeds.SportRadar.NHL.Interfaces;

namespace SportRanker.Feeds.SportRadar.NHL.Infrastructure
{
    public class Publisher : IPublisher
    {
        private const string NewFixtureExchange = "new_fixture_exchange";

        private const string NewNHLFixtureRoutingKey = "results.nhl";

        private const string CloudAMPQUrl = @"amqps://lhqadfns:Ox1Z9RVKMsu36ZjbLV0HEzknWsgJi36S@raven.rmq.cloudamqp.com/lhqadfns";

        public void PublishFixtureResult(FixtureResult fixtureResult)
        {
            Uri ampqUri = new Uri(CloudAMPQUrl);

            var factory = new ConnectionFactory()
            {
                Uri = ampqUri
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(
                    exchange: NewFixtureExchange,
                    type: "topic");

                var message = JsonConvert.SerializeObject(fixtureResult);

                var body = Encoding.UTF8.GetBytes(message);

                try
                {
                    channel.BasicPublish(exchange: NewFixtureExchange,
                        routingKey: NewNHLFixtureRoutingKey,
                        basicProperties: null,
                        body: body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could Not Publish To Queue");
                }

                Thread.Sleep(100);
            }
        }
    }
}
