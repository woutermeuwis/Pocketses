# pull official base image
FROM node:alpine as build-stage
RUN mkdir /app
WORKDIR /app
COPY package.json .
COPY package-lock.json .
RUN npm install
COPY . /app
RUN npm run build

FROM nginx:alpine as prod-stage
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage /app/dist /app
CMD ["nginx", "-g", "daemon off;"]