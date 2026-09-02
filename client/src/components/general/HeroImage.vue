<template>
    <div class="hero-image">
        <BusinessLogo v-if="props.showLogo" color="black" />

        <div
            v-if="props.text"
            class="hero-text"
            :class="`hero-text-${props.direction}`"
        >
            <h1>{{ props.text }}</h1>

            <div v-if="props.links?.length" class="hero-actions">
                <NuxtLink
                    v-for="link in props.links"
                    :key="link.link"
                    class="hero-action"
                    :to="link.link"
                >
                    {{ link.text }}
                </NuxtLink>
            </div>
        </div>

        <!-- Custom content -->
        <div
            v-if="$slots.default"
            class="hero-content"
            :class="`hero-content-${props.direction}`"
        >
            <slot />
        </div>
    </div>
</template>

<script setup lang="ts">
import type { Link } from '~/types/Link'
import BusinessLogo from '../business/BusinessLogo.vue'

const props = withDefaults(
    defineProps<{
        image?: string
        text?: string
        links?: Link[]
        height?: string
        showLogo?: boolean
        direction?: 'column' | 'row'
    }>(),
    {
        image: '',
        text: '',
        links: () => [],
        height: '350px',
        showLogo: false,
        direction: 'column'
    }
)
</script>

<style scoped>
.hero-image {
    background-image: v-bind("`url(${props.image})`");
    background-repeat: no-repeat;
    background-position: center center;
    background-size: cover;
    background-blend-mode: lighten;

    width: 100%;
    min-height: 350px;
    min-height: v-bind("props.height");
    height: auto;
    overflow: hidden;

    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;

    gap: 40px;
    position: relative;
}

/* -------------------------
   Default text
------------------------- */

.hero-text {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 20px;

    text-align: center;
    color: white;
    font-size: 40px;
    font-family: 'Baliva';

    flex-wrap: wrap;
    z-index: 10;
}

.hero-text h1 {
    margin: 0;
}

/* -------------------------
   Links
------------------------- */

.hero-actions {
    display: flex;
    gap: 10px;
    flex-wrap: wrap;
    justify-content: center;
}

.hero-action {
    border: 2px solid white;
    background: rgba(10, 10, 10, 0.5);
    display: inline-block;

    padding: 10px 25px;

    color: white;
    text-align: center;
    cursor: pointer;
}

.hero-action:hover {
    background-color: #555;
    color: white;
}

/* -------------------------
   Slot
------------------------- */

.hero-content {
    display: flex;
    align-items: center;
    justify-content: center;

    gap: 20px;
    width: 100%;
}

.hero-content > *{
  margin: 10px;
  box-sizing: border-box;
}

.hero-content-column {
    flex-direction: column;
}

.hero-content-row {
    flex-direction: row;
}

/* -------------------------
   Mobile
------------------------- */

@media screen and (max-width: 480px) {
    .hero-text {
        flex-direction: column;
        font-size: 28px;
    }

    .hero-content-row {
        flex-direction: column;
    }
}
</style>