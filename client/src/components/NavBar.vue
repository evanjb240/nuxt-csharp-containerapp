<script setup lang="ts">
import { MenuIcon } from '@zhuowenli/vue-feather-icons'
import { useAuth0 } from '@auth0/auth0-vue';

const runtimeConfig = useRuntimeConfig();
const authEnabled = runtimeConfig.public.enableAuth0 as boolean;
const auth = authEnabled ? useAuth0() : null;
const isAuthenticated = authEnabled ? auth?.isAuthenticated : ref(false);
const isLoading = authEnabled ? auth?.isLoading : ref(false);
const nav = ref<string>("topnav");

function navBar(){
    if(nav.value === "topnav"){
        nav.value += ' responsive';
    }else{
        nav.value = 'topnav';
    }
}

function login() {
    if (!import.meta.client || !authEnabled || !auth) return;

    auth.loginWithRedirect({ authorizationParams: {
        redirect_uri: window.location.origin
    }});        
}
function authLogout() {
    if (!import.meta.client || !authEnabled || !auth) return;

    auth.logout({
        logoutParams: {
            returnTo: window.location.origin
        }
    });
}
</script>

<template>
    <nav class="nav" :class="nav">
        <div class="nav-bar-container">
            <div class="nav-button-container">
                <NuxtLink to="/" @click="nav = 'topnav'">        
                    <BusinessLogo not-centered :width="50" :height="23" color="white"/>
                </NuxtLink>
                <NuxtLink to="/About" @click="nav = 'topnav'">About</NuxtLink>
                <NuxtLink to="/ContactUs" @click="nav = 'topnav'">Contact</NuxtLink>
                <NuxtLink v-if="isAuthenticated && !isLoading"  to="/Profile" @click="nav = 'topnav'">Profile</NuxtLink>
            </div>
            <div class="row menu-container">
                <button v-if="authEnabled && !isAuthenticated && !isLoading" class="auth" @click="login">Log in</button>
                <button v-if="authEnabled && isAuthenticated && !isLoading" class="auth" @click="authLogout">Log out</button>

                <a href="javascript:void(0);" @click="navBar" class="icon">
                    <menu-icon size="16"></menu-icon>
                </a>
            </div>
        </div>
    </nav>
</template>

<style scoped>
.nav{
    position:fixed;
    top:0;
    width:100%;
    z-index: 99;
    display: flex;  
    flex-direction: row;
    align-items: center;
    justify-content: center;
}

.nav-bar-container{
    width:95%;
    display: flex;  
    flex-direction: row;
    align-items: center;
    justify-content: space-between;
}

.nav-button-container{
    display: flex;  
    flex-direction: row;
    align-items: center;
    justify-content: center;
}

.row{
    display: flex;
    flex-direction: row;
}

.topnav {
    overflow: hidden;
    background-color: #333;
}

.topnav a {
    float: left;
    display: block;
    color: #f2f2f2;
    text-align: center;
    padding: 10px 12px;
    text-decoration: none;
    font-size: 17px;
}

button{
    display: block;
    color: #f2f2f2;
    border-color: #f2f2f2;
    background-color: #333;
    text-align: center;
    padding: 5px 8px;
    margin-right:10px;
    text-decoration: none;
    font-size: 17px;
}

.topnav a:hover {
    background-color: #ddd;
    color: black;
}

.topnav a.router-link-active:not(:first-child) {
  background-color: rgba(5, 32, 58, 0.75);
  color: white;
}

.topnav .icon {
    display: none;
}

@media screen and (max-width: 1000px) {
    .topnav a:not(:first-child) {
        display: none;
    }

    .topnav a.icon {
        float: right;
        display: block;
    } 
}

@media screen and (max-width: 1000px) {
    .topnav.responsive {
        position: fixed;
    }

    .topnav.responsive .nav-bar-container{
        display: block;
    }

    .nav-button-container{
        display: block;
    }

    .topnav.responsive .menu-container {
        float: right;
        position: absolute;
        right: 2.5%;
        top: 3px;
    }

    .topnav.responsive a {
        float: none;
        display: block;
        text-align: left;
    }
}
</style>