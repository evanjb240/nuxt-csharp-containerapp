<template>
  <div v-if="user" class="container">
    <div class="row align-items-center profile-header">
      <div class="col-md-2 mb-3">
        <img
          :src="user?.picture"
          alt="User's profile picture"
          class="rounded-circle img-fluid profile-picture"
        />
      </div>
      <div v-show="isAuthenticated" class="col-md text-center text-md-left">
        <h2>{{ user?.name }}</h2>
        <p>Nickname: {{ user?.nickname }}</p>
        <p>Email: {{ user?.email }}</p>
        <p>Last updated: {{ user?.updated_at }}</p>
      </div>
    </div>

    <div class="row">
      <div>{{ JSON.stringify(user, null, 2) }}</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Auth0Context } from '~/composables/Auth0Context';

  const { user, isAuthenticated } = Auth0Context();
</script>