const axios = require("axios");

export class API {
  constructor(controllerName) {
    this._apiUrl = this.getApiPath() + controllerName;
  }
  get apiUrl() {
    return this._apiUrl;
  }

  set apiUrl(apiUrl) {
    this._apiUrl = apiUrl;
  }

  getApiPath() {
    var apiPath = null;
    const servers = {
      localhost: "http://localhost:3131/api/",
      test: "https://cai1-sv00035.vnet.valeo.com:8443/medicaltool/api/",
      prod: "https://cai1-sv00008.vnet.valeo.com:8443/medicaltool/api/"
    };

    if (window.location.host.includes("localhost")) {
      apiPath = servers["localhost"];
    }
    if (window.location.host.includes("cai1-sv00035")) {
      apiPath = servers["test"];
    }
    //  else if (window.location.host.includes("cai1-sv00035")) {
    //   apiPath =
    //     "https://cai1-sv00035.vnet.valeo.com:8095" +
    //     window.location.pathname +
    //     "api/";
    // } else if (window.location.host.includes("cai1-sv00008")) {
    //   apiPath =
    //     "https://cai1-sv00008.vnet.valeo.com:8095" +
    //     window.location.pathname +
    //     "api/";
    // }
    return apiPath;
  }


  get(requestBody) {
    return axios
      .get(this.apiUrl, requestBody)
      .then(result => {
        // const { status, message } = result.data;
        // if (!status) return Promise.reject({ message });
        return Promise.resolve(result);
      })
      .catch(error => {
        console.error(error);
        return Promise.reject(error);
      });
  }
  post(config, requestData = null) {
    return axios
      .post(this.apiUrl, requestData, config)
      .then(result => {
        // const { status, message } = result.data;
        // if (!status) return Promise.reject({ message });
        return Promise.resolve(result);
      })
      .catch(error => {
        console.error(error);
        return Promise.reject(error);
      });
  }

  put(config, requestBody) {
    return axios
      .put(this.apiUrl, requestBody, config)
      .then(result => {
        return Promise.resolve(result);
      })
      .catch(error => {
        console.error(error);
        return Promise.reject(error);
      });
  }

  delete(requestBody) {
    return axios
      .delete(this.apiUrl, requestBody)
      .then(result => {
        return Promise.resolve(result);
      })
      .catch(error => {
        console.error(error);
        return Promise.reject(error);
      });
  }
}