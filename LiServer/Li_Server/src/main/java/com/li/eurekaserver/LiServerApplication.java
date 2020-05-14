package com.li.eurekaserver;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.netflix.eureka.server.EnableEurekaServer;

@EnableEurekaServer
@SpringBootApplication
public class LiServerApplication {
    public static void main(String[] args) {
        SpringApplication.run(LiServerApplication.class, args);
    }
}
