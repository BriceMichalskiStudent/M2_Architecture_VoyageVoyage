FROM node:latest
COPY . /app
WORKDIR /app
RUN npm install -g yarn \
    && yarn build \
    && yarn start
