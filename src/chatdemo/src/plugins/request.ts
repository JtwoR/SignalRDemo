import axios from 'axios';

const request=axios;
request.defaults.baseURL="http://192.168.31.194:5000/";


export default request;