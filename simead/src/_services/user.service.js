import config from 'config';
import { authHeader } from '../_helpers';
import { fbind } from 'q';

export const userService = {
    login,
    logout,
    register,
    getAll,
    getById,
    update,
    delete: _delete
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch(`${config.apiUrl}/users/authenticate`, requestOptions)
        .then(handleResponse)
        .then(user => {
            if (user.token) {
                localStorage.setItem('user', JSON.stringify(user));
            }

            return user;
        });
}

function logout() {
    localStorage.removeItem('user');
}

async function register(user) {
    const requestOptions = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(user);
    };

    let handleResponse = await fetch(`${config.apiUrl}/users/register`, requestOptions);
    return handleResponse(handleResponse);
}

async function getAll() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let handleResponse = await fetch(`${config.apiUrl}/users`, requestOptions);
    return handleResponse(handleResponse);
}

async function getById(id) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };

    let handleResponse = await fetch(`${config.apiUrl}/users/${id}`, requestOptions);
    return handleResponse(handleResponse);
}

async function update(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(), 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    let handleResponse = await fetch(`${config.apiUrl}/users/${user.id}`, requestOptions);
    return handleResponse(handleResponse);
}

async function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    let handleResponse = await fetch(`${config.apiUrl}/users/${id}`, requestOptions);
    return handleResponse(handleResponse);
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);

        if (!response.ok) {
            if (response.status === 401) {
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    })
}