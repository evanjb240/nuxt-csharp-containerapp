<template>
    <!-- add `data-active` and the event listeners -->
    <div :data-active="active" @dragenter.prevent="setActive" @dragover.prevent="setActive" @dragleave.prevent="setInactive" @drop.prevent="onDrop">
        <!-- share state with the scoped slot -->
        <slot :dropZoneActive="active"></slot>
    </div>
</template>

<script setup lang="ts">
// make sure to import `ref` from Vue
import { ref, onMounted, onUnmounted } from 'vue'
const emit = defineEmits(['files-dropped'])

// Create `active` state and manage it with functions
let active = ref(false)
let inActiveTimeout: any  = null

function setActive() {
    active.value = true
    clearTimeout(inActiveTimeout) // clear the timeout
}
function setInactive() {
    // wrap it in a `setTimeout`
    inActiveTimeout = setTimeout(() => {
        active.value = false
    }, 50)
}

// Nothing below this changes

function onDrop(e: any) {
    emit('files-dropped', [...e.dataTransfer.files])
}

function preventDefaults(e: Event) {
    e.preventDefault()
}

const events = ['dragenter', 'dragover', 'dragleave', 'drop']

onMounted(() => {
    events.forEach((eventName) => {
        document.body.addEventListener(eventName, preventDefaults)
    })
})

onUnmounted(() => {
    events.forEach((eventName) => {
        document.body.removeEventListener(eventName, preventDefaults)
    })
})
</script>
