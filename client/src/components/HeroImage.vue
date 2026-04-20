<template>
    <div class="hero-image">
        <div>
          <BusinessLogo v-if="props.showLogo" color="white" />
        </div>
        <div v-if="props.text" class="hero-text">
            <h1>{{ text }}</h1>
        </div>
        <div v-if="props.links" class="hero-text">
            <NuxtLink class="hero-action" v-for="link in props.links" :to="link.link">{{ link.text }}</NuxtLink>
        </div>
    </div>
</template>
<script setup lang="ts">
import type { Link } from '~/types/Link';
import BusinessLogo from './BusinessLogo.vue';

const props = defineProps({
    image: { type: String, default: '' },
    text:{ type:String, default: ''},
    links: { type:Array<Link>, default: []},
    height: { type:String, default: '350px' },
    showLogo: { type: Boolean, default: false }
})
</script>
<style scoped>
.hero-image {
  background: v-bind("props.image") no-repeat center center; 
  background-size: cover;
  background-blend-mode: lighten;
  min-height:350px;
  display:flex;
  flex-direction: column;
  align-items:center;
  justify-content: center;
  gap: 100px;
  position: relative;
}

.hero-text {
  display: flex;
  gap: 10px;
  text-align: center;
  color: white;
  font-size: 40px;
  font-family: 'Baliva';
  flex-wrap: wrap;
  justify-content: center;
  z-index: 10;
}

.hero-text .hero-action {
  border: 2px solid white;
  background: rgba(10, 10, 10, 0.5);
  outline: 1;
  display: inline-block;
  padding: 10px 25px;
  color: white;
  text-align: center;
  cursor: pointer;
}

.hero-text .hero-action:hover {
  background-color: #555;
  color: white;
}

@media screen and (min-width: 480px) {
  .hero-image {
    height: v-bind("props.height");
  }
}
@media screen and (max-width: 480px) {
  .hero-text{
    flex-direction: column;
    font-size: 28px;
  }
}
</style>