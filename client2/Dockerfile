FROM node:18-buster 


WORKDIR /client2/src/app

COPY . /client2/src/app

RUN npm install -g @angular/cli

RUN npm install

RUN ng analytics off


CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]
