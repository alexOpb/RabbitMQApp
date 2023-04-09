# RabbitMQApp

To run:

1. Open terminal in solution folder.
2. Run docker-compose up -d to start RabbitMQ.
3. Open RabbitMQ Managment UI: `http://localhost:15672`
4. Run RabbitPublisher project and check SampleExchange in Exchange tab and messages in Queues tab.
5. Launch consumer projects and see messages disappear from the queue as they are consumed.
