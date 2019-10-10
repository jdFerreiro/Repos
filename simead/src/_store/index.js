import Vue from 'vue';
import Vuex from 'vuex';
import { alert } from './alert.module';
import { accout } from './account.module';
import { users } from './users.module';

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    alert,
    accout,
    users
  }
});
