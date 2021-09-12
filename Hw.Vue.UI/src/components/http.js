import axios from "axios";
import { Message } from "view-design";


axios.defaults.headers["Content-Type"] = "application/json;charset=utf-8";
// 创建axios实例 axios.create([config])
const service = axios.create({
    // axios中请求配置有baseURL选项，表示请求URL公共部分

    // baseURL:
    //     process.env.NODE_ENV === "production" //正式生产环境，程序最终发布后所需要的参数配置
    //         ? process.env.VUE_APP_BASE_URI
    //         : process.env.VUE_APP_BASE_API,

    // 超时
    timeout: 30000
});
// request拦截器
service.interceptors.request.use(
    config => {
        // 是否需要设置 token
        console.log(`${config.method} : ${config.url}`);
        config.headers["token"] = sessionStorage.getItem('toKenValue'); // 让每个请求携带自定义token 请根据实际情况自行修改
        return config;
    },
    error => {
        console.log(error);
        Promise.reject(error);
    }
);




service.defaults.baseURL = "https://localhost:5001/Api/";
service.interceptors.response.use(
    response => {
        console.log(response.data);
        // 如果返回的状态码为200，说明接口请求成功，可以正常拿到数据
        if (response.status === 200 && response.data.state == 400) {
            Message.error("失败");
        } else if (response.status === 200 && response.data.state == 401) {
            Message.error("更新失败");
        } else if (response.status === 200 && response.data.state == 402) {
            Message.error("保存失败");
        } else if (response.status === 200 && response.data.state == 403) {
            Message.error("删除失败");
        }
        return Promise.resolve(response);
    },
    // 服务器状态码不是2开头的的情况
    // 这里可以跟你们的后台开发人员协商好统一的错误状态码
    // 然后根据返回的状态码进行一些操作，例如登录过期提示，错误提示等等
    // 下面列举几个常见的操作，其他需求可自行扩展
    error => {
        if (error.response.status) {
            switch (error.response.status) {
                // 401: 未登录
                // 未登录则跳转登录页面，并携带当前页面的路径
                // 在登录成功后返回当前页面，这一步需要在登录页操作。
                case 401:

                    Message.info("身份验证失败，请关闭重新进入。");
                    break;

                // 403 token过期
                // 登录过期对用户进行提示
                // 清除本地token和清空vuex中token对象
                // 跳转登录页面
                case 403:
                    Message.info("登录过期，请关闭重新进入。");
                    // 清除token
                    break;

                // 404请求不存在
                case 404:
                    Message.info("您访问的网页不存在。");
                    break;
                // 其他错误，直接抛出错误提示
                default:
                    Message.info(error.response.data.message);
            }
            return Promise.reject(error.response);
        }
    }
);

export default service;