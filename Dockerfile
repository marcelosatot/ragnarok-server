FROM debian:bookworm

ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y \
    build-essential \
    cmake \
    git \
    pkg-config \
    default-libmysqlclient-dev \
    zlib1g-dev \
    libpcre3-dev \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /rathena

COPY rathena/ .

RUN mkdir -p build \
 && cd build \
 && cmake .. \
 && make -j"$(nproc)"

WORKDIR /rathena

CMD ["/bin/bash"]
