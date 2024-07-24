import { createStore } from 'vuex';
import { loginApiData as authLogin } from '@/services/dotnetapi';

export default createStore({
  state: {
    user: null,
    token: null
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
    },
    setToken(state, token) {
      state.token = token;
    },
    clearAuthData(state) {
      state.user = null;
      state.token = null;
    }
  },
  actions: {
    async loginTo({ commit }, authData) {
      try {
        const response = await authLogin(authData);
        const token = response.data.token;
        const user = authData.email;
        commit('setToken', token);
        commit('setUser', user);
      } catch (error) {
        throw new Error(error.response?.data?.message || 'Failed to login');
      }
    },
    logoutTo({ commit }) {
      commit('clearAuthData');
    }
  },
  getters: {
    isAuthenticated(state) {
      return state.token !== null;
    },
    user(state) {
      return state.user;
    }
  }
})
