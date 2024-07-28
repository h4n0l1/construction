import axios from 'axios';

const symfonyApi = axios.create({
  baseURL: import.meta.env.VITE_APP_BASE_URL_SYMFONY
});

export const getApiData = () => {
  return symfonyApi.get('/constructions');
};

export const getApiDataById = id => {
  return symfonyApi.get(`/constructions/${id}`);
};
