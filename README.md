# API

## Deploy to Heroku

```
heroku login
heroku container:login
docker build -t YourAppName .
heroku container:push -a YourAppName web
heroku container:release -a YourAppName web
```