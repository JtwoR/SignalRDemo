# SignalRDemo
基于.netcore的signalr实现即时通信Demo

[![Sqlite](https://img.shields.io/badge/.NETCore-3.1-important.svg?style=plastic)]()
[![vue](https://img.shields.io/badge/vue-2.0+-brightgreen.svg?style=plastic)](https://cn.vuejs.org/)
[![Sqlite](https://img.shields.io/badge/sqlite-3-blue.svg?style=plastic)]()

前端插件:  
[聊天富文本输入框](https://github.com/jrainlau/chat-input-box)

---
* ### 说明

    >1、前端项目初始化运行:
    ```shell
    cd src\chatdemo

    npm install

    npm run serve
    ```

    >2、当前项目使用本地的sqlite做存储，在ChatDemo目录下的chat.db

    >3、生成的头像会直接生成到webapi项目下的wwwroot目录
---
* ### 效果图
![result.png](https://github.com/JtwoR/SignalRDemo/blob/main/result.png)

---
* ### To do
    - [x] 消息群送
    - [x] 头像生成
    - [ ] 默认表情框   
    - [ ] 房间逻辑