FROM nginx:1.27-alpine

# Custom config for Unity WebGL hosting.
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Unity WebGL build output.
COPY Build/WebGL/ /usr/share/nginx/html/

EXPOSE 80
