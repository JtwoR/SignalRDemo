<template>
  <!-- <div class="home-body" >
    <v-container>
      <v-row class="d-flex flex-row  h-100" style="background-color:white;border-radius: 1.5px;">
        <v-col cols="12" sm="3" class="order-first" >
          <div class="w-100 d-flex flex-column" style="border-right:0.8px solid #dddddd;height:709px">
            <div  style="height:49px;border-bottom:0.8px solid #dddddd;"></div>
            <div class="d-flex flex-column h-100" style="overflow-y:auto;height:660px;">
                <div class="d-flex flex-row " style="border-bottom:0.8px solid #dddddd;padding:8px 0;" v-for="(item,i) in room_list" :key="i" >
                    <v-col cols="12" sm="4" class="d-flex" style="justify-content:center ">
                      <img :src="profile"  style="height:40px;width:40px;"/>
                    </v-col>
                    <v-col cols="12" sm="8" >

                    </v-col>
                  </div>
            </div>
            </div>
        </v-col>
        <v-col cols="12" sm="9" class="order-0">
          <v-container style="padding:0">
            <div class="w-100 d-flex flex-column" style="height:709px">
              <v-col cols="12" sm="1" style="max-width:100%;border-bottom:0.8px solid #dddddd;"></v-col>
              <v-col cols="12" sm="8" style="max-width:100%;border-bottom:0.8px solid #dddddd;">
              </v-col>
              <v-col cols="12" sm="3" style="max-width:100%"></v-col>
            </div>
          </v-container>
          
        </v-col>
      </v-row>
    </v-container>
  </div> -->
  <v-app id="inspire">

    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>Application</v-toolbar-title>
    </v-app-bar>
    <v-navigation-drawer
      v-model="drawer"
      app
    >
      <v-sheet
        color="grey lighten-4"
        class="pa-4"
      >
        <v-avatar
          class="mb-4"
          color="grey darken-1"
          size="64"
        ></v-avatar>

        <div>john@vuetifyjs.com</div>
      </v-sheet>

      <v-divider></v-divider>

      <v-list>
        <v-list-item
          v-for="[icon, text] in links"
          :key="icon"
          link
        >
          <v-list-item-icon>
            <v-icon>{{ icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ text }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <v-container
        class="py-8 px-6"
        fluid
      >
        <v-row>
          <v-col
            v-for="card in cards"
            :key="card"
            cols="12"
          >
            <v-card>
              <v-subheader>{{ card }}</v-subheader>

              <v-list two-line>
                <template v-for="n in 6">
                  <v-list-item

                    :key="n"
                  >
                    <v-list-item-avatar color="grey darken-1">
                    </v-list-item-avatar>

                    <v-list-item-content>
                      <v-list-item-title>Message {{ n }}</v-list-item-title>

                      <v-list-item-subtitle>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nihil repellendus distinctio similique
                      </v-list-item-subtitle>
                    </v-list-item-content>
                  </v-list-item>

                  <v-divider
                    v-if="n !== 6"
                    :key="`divider-${n}`"
                    inset
                  ></v-divider>
                </template>
              </v-list>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
const signalR = require("@microsoft/signalr");
export default {
  data() {
    return {
      connection: "",
      user: "",
      message: "",
      msgList: [],
      profile:require("@/assets/img/profile.jpg"),
      room_list:[{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{}],
      cards: ['Today', 'Yesterday'],
      drawer: null,
      links: [
        ['mdi-inbox-arrow-down', 'Inbox'],
        ['mdi-send', 'Send'],
        ['mdi-delete', 'Trash'],
        ['mdi-alert-octagon', 'Spam'],
      ],
    };
  },
  created() {
    this.init();
  },
  methods: {
    init() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/chathub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
        .configureLogging(signalR.LogLevel.Error)
        .build(); 

      this.connection.on("ReceiveMessage", data => {
        this.msgList.push(data);
      });
      
      this.connection.start();
    },
    sendMsg() {
      let params = {
        user: this.user,
        message: this.message
      };
      this.connection.invoke("SendMessage", params);
    }
  }
};
</script>



<style>
  p{
    margin: 0 !important;
    padding:0 !important;
  }


  .home-body{
    min-width: 360px;max-width: 1264px ;margin: 0 auto;height: 100%;
  }

  .home-right-col{
    padding:30px 20px;
  }
  
  .v-icon.mdi-minus{
    transform: rotate(90deg);
  }

  .h-100{
    height: 100%;
  }

  .w-100{
    width: 100%;
  }
 
</style>
