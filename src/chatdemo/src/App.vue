<template>
  <div id="app"  >
    <v-app id="inspire">
    <v-app-bar-nav-icon class="d-flex d-sm-none" @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
    
    <v-navigation-drawer 
        v-model="drawer"
        absolute
        left
        temporary
      >
        <v-list nav dense>
          <v-list-item-group
            v-model="group"
            active-class="deep-blue-grey--text text--accent-4"
          >
            <v-list-item>
              <!-- <v-list-item-title to="/home">Home</v-list-item-title> -->
            </v-list-item>
          </v-list-item-group>
        </v-list>
      </v-navigation-drawer>

      <v-main>
        <v-container class="webcontainer h-100 w-100">
          <v-row>
            <v-col cols="12" md="2" sm="0"></v-col>
            <v-col cols="12" md="8" sm="12">
              <v-row>
                <v-col cols="12" md="3" sm="0" xs="0" v-if="show" >
                  <v-row class="h-100">
                    <v-col cols="12" md="2">
                      <div class=" h-100" style="background:#28292b;text-align:center">
                        <v-list-item-avatar style="margin-top:15px" >
                          <v-img :src="avatar"></v-img>
                        </v-list-item-avatar>
                        <div style="padding:15px 0;"></div>
                        <div style="display:flex;flex-direction: column;">
                          <v-icon class="l-icon">{{icons.mdiChatProcessing}}</v-icon>
                          <v-icon class="l-icon">{{icons.mdiCog}}</v-icon>
                        </div>
                        
                      </div>
                    </v-col>
                    <v-col cols="12" md="10">
                      <div class=" h-100" style="background:#ebe9e7">
                        <div style="height:64px;padding:0 20px;"><v-text-field label="Search"></v-text-field></div>
                        <div style="height:727px">
                          <div>
                            <v-row style="padding:5px 10px;background:#c7c5c4">
                              <v-col cols="12" md="3">
                                <v-list-item-avatar  tile >
                                  <v-img :src="testRoomImg"></v-img>
                                </v-list-item-avatar>
                              </v-col>
                              <v-col cols="12" md="9">
                                <v-row>
                                  <v-col cols="12" md="12" style="text-align:left;margin-top:8px">🐟🐟🐟</v-col>
                                  <v-col cols="12" md="12"></v-col>
                                </v-row>
                              </v-col>
                            </v-row>
                          </div>
                        </div>
                      </div>
                    </v-col>
                  </v-row>
                </v-col>
                <v-col cols="12" md="9" sm="12"> 
                 <div  >  <!-- style="position: relative;text-align:left;z-index:999" position: absolute;--> 
                    <div class="w-100 room-title">{{roomTitle}}</div>
                  </div>
                  
                  <div class="content">
                    <div class="chatcontent" :style="{'max-height':contentMaxHeight,'min-height':contentMaxHeight}">
                      <div v-for="(m,index) in message" :key="index">
                        <div class="message-item">
                          
                          <div style="margin-right:10px">
                            <v-list-item-avatar size="33" >
                              <v-img :src="request.defaults.baseURL+m.avatar"></v-img>
                            </v-list-item-avatar>

                          </div>
                          
                          <div class="message-item-right" style="  display: flex; align-items: center;">
                            <div class="message-content" :style="{'max-width':msgMaxWidth}" v-html="m.message"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                     <v-divider></v-divider>
                    <div style="display:flex;flex-direction: row;background:white;padding:5px 15px 0 15px;">
                        <v-icon class="pr-5">{{icons.mdiRobotConfusedOutline}}</v-icon>
                        <v-icon class="pr-5">{{icons.mdiCog}}</v-icon>
                    </div>
                    <div class="input-area" :style="{'max-height':textMaxHeight,'min-height':textMaxHeight}">
                      <!-- <textarea  class=" h-100 w-100" type="text" style="padding:5px;resize:none;" v-model="sendMsgContent" @keyup.enter="sendMsg" > </textarea> -->
                      <div
                          class="editor h-100"
                          contenteditable="true"
                          ref="editor"
                          @keyup="getCursor"
                          @keydown.enter.prevent="submit"
                          @paste.prevent="onPaste"
                          @click="getCursor"
                          :style="{'max-height':textMaxHeight,'min-height':textMaxHeight,'text-align':'left','padding':'8px 15px'}">
                      </div>
                    </div>
                  </div> 
                </v-col>

              </v-row>
            </v-col>
            <v-col cols="12" md="2" sm="0"></v-col>
          </v-row>
          
        </v-container>
      </v-main>
    </v-app>
  </div>
</template>

<style scoped>
@import 'assets/scss/App.scss';
</style>

<script lang="ts">
import Vue from 'vue';
import { mdiChatProcessing,mdiCog,mdiRobotConfusedOutline } from "@mdi/js";
import request from './plugins/request';
const signalR = require("@microsoft/signalr");

import paste from './plugins/paste.js'
import emoji from './plugins/emoji.js'
import { getCursorPosition, setCursorPosition } from './plugins/cursorPosition.js'

export default Vue.extend({
  name: 'App',

  components: {

  },

  data: () => ({
    drawer:false,
    group: null,
    mini:true,
    cards: ['Today', 'Yesterday'],
    icons:{mdiChatProcessing,mdiCog,mdiRobotConfusedOutline},
    message:[
    ],
    connection:{},
    items:[{"icon":"","title":"测试"}],
    show:true,
    screenWidth:0,
    screenHeight:0,
    search:"",
    contentMaxHeight:'',
    textMaxHeight:'',
    msgMaxWidth:'',
    sendMsgContent:'',
    guid:'',
    avatar:'',
    request:{},

    editor: null as any,
    cursorPosition: 0,
    emoji:{},
    roomTitle:'🐟🐟🐟',
    testRoomImg:require(".//assets/img/profile.jpg")
  }),
  watch: {
      group () {
        this.drawer = false
      },
      screenWidth: function (n, o) {
            if (n < 960) {
              this.show=false;

              const h=468;
              this.contentMaxHeight=h+'px';
              this.textMaxHeight=188+'px';

              this.msgMaxWidth=200+'px';

            } else {
              
              this.show=true;
              this.contentMaxHeight=558+'px';
              this.textMaxHeight=168+'px';

              this.msgMaxWidth=500+'px';
            }
      },
      screenHeight:function(n,o){

      }
    },
  created(){
    //this.$vuetify.theme.dark=false;
    this.request=request;
    this.init();

    this.getMessage();

  },
  methods:{
    init() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(request.defaults.baseURL+"chathub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
        .configureLogging(signalR.LogLevel.Error)
        .build(); 

      this.connection.on("ReceiveMessage", (data) => {
        this.message.unshift(data);
      });
      
      this.connection.start().then(()=>{
         if(this.$route.query.test=="1"){
          
          }

          let check=this.getCookie("guid");
          if(check==null||check==''){
            this.guid=this.getGUID();
            this.setCookie("guid",this.guid);
            this.register();
          }else{
            this.guid=this.getCookie("guid")??"";
          }
          console.log(this.guid);
          this.getAvatar();
      });

      console.log(this.connection);
    },
    sendMsg(){//发送消息
      var obj={message:this.sendMsgContent,guid:this.guid};
      this.sendMsgContent='';
      this.connection.invoke("SendMessage", obj).catch(function (err) {
        return console.error(err.toString());
      });
      
    },
    getGUID(){//生成GUID
      function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
      }
      return (S4() + S4() + S4() + S4() + S4() + S4() + S4() + S4());
    },
    getAvatar(){//获取头像
      request.get('/api/user/avatar?guid='+this.guid).then((response:any)=> {
        if (response != null && response.data.code == 200) { this.avatar=request.defaults.baseURL+'/image/'+response.data.data;}
      }).catch((error:any) =>{
        console.log(error);
      });
    },
    register(){//注册
      request.get('/api/user/register?guid='+this.guid).then((response:any)=> {
        
      }).catch((error:any) =>{
        console.log(error);
      });
    },
    getMessage(){//进入房间获取旧通信信息
      request.get('/api/ChatRoom/message').then((response:any)=> {
          if (response != null && response.data.code == 200) {this.message=response.data.data; }
      }).catch((error:any) =>{
        console.log(error);
      });
    },
    setCookie(name:string,value:string)
    {
        document.cookie = name + "=" + escape (value);
    },
    getCookie(name:string)
    {    
        var arr,reg=new RegExp("(^| )"+name+"=([^;]*)(;|$)");
        if(arr=document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return null;
    },
    submit (e:any) {
      const value = e.target.innerHTML.replace(/[\n\r]$/, '')
      if (value) {
        e.target.innerText = '';
        var obj={message:value,guid:this.guid};
        this.connection.invoke("SendMessage", obj).catch(function (err) {
          return console.error(err.toString());
        });
        
      }
    },
    async onPaste (e:any) {
      const result = await paste(e)
      const imgRegx = /^data:image\/(?:jpeg|jpg|png);base64,/
      if (imgRegx.test(result)) {
        // const sel = window.getSelection()
        // if (sel && sel.rangeCount === 1 && sel.isCollapsed) {
        //   const range = sel.getRangeAt(0)
        //   const img = new Image()
        //   img.src = result
        //   range.insertNode(img)
        //   range.collapse(false)
        //   sel.removeAllRanges()
        //   sel.addRange(range)
        // }

        document.execCommand('insertImage', false, result)
      } else {
        document.execCommand('insertText', false, result)
      }
    },
    getCursor () {
      this.cursorPosition = getCursorPosition(this.editor)
    },
    insertEmoji (emoji:any) {
      const text = this.editor.innerHTML
      this.editor.innerHTML = text.slice(0, this.cursorPosition) + emoji + text.slice(this.cursorPosition, text.length)
      setCursorPosition(this.editor, this.cursorPosition + 1)
      this.cursorPosition = getCursorPosition(this.editor) + 1 //  emoji takes 2 bytes
    }
  },
  mounted() {
        this.editor = this.$refs['editor'];
        //获取屏幕尺寸
        this.screenWidth = document.body.clientWidth;
        this.screenHeight = document.body.clientHeight;
        window.onresize = () => {
            //屏幕尺寸变化
            return (() => {
                this.screenWidth = document.body.clientWidth
                this.screenHeight = document.body.clientHeight
                console.log(document.body)
            })()
        }
    },
});
</script>


