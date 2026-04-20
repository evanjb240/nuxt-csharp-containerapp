import type { User } from "@auth0/auth0-vue";

export const Auth0Context = () => {
    return {
        user: useState('auth-user') as User,
        isAuthenticated: useState('is-authenticated').value as boolean,
        isLoading: useState('auth-loading').value as boolean,
        roles: useState('auth-roles')
    };
};