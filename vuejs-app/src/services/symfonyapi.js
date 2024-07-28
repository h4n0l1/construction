import axios from 'axios';

const symfonyApi = axios.create({
  baseURL: 'https://symfony.dennytriana.com/api'
});

export const getApiData = () => {
  return symfonyApi.get('/constructions');
};

export const getApiDataById = id => {
  return symfonyApi.get(`/constructions/${id}`);
};
