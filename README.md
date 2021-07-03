# Intellias Hackaton

## Summary

Solution was implemented using three-layer architecture:
1. Presentation layer - web api.
2. Application layer - services which includes main business logic.
3. Data layer - MongoDb database with appropriate api for receiving data.

As a database I decide to use NoSql database - MongoDb. It can be quickly configured in docker.
Since in requirements all queries are based on user I decided that such entity must be fully filed with all data (one user - one complex document), including groups, flows and videos. It allows me avoid any joins during query
Of course, such approach also is not free from drawbacks: during changes it could be will take more time and resources for document(s) reindexation.
## Run

<Put here steps to run your solution>

_Example_

```bash
# Go into the folder with solution and run:
$ docker-compose up
```

You can reach api using 52944 port.
Url example: https://localhost:52944/users/5/videos/7?priority=asc

RabbitMqPort: 15672
Username:guest
Password:guest

## Test

Unfortunately not enough time for test.

## Notes

P.S. I know that RabbitMq could be configured in more advanced way and it is better to send message in separate middleware or action filter, but not enough time for it and I'm exhausted)