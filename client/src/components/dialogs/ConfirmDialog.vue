<template>
  <Teleport to="body">
    <div v-if="open" class="overlay" @click="onCancel">
      <div class="dialog">
        <h3>{{ header }}</h3>
        <p>{{ message }}</p>

        <div class="actions">
          <button class="primary-btn" @click="onCancel">Cancel</button>
          <button class="danger-btn" @click="onConfirm">Confirm</button>
        </div>
      </div>
    </div>
  </Teleport>
</template>

<script setup lang="ts">
import { ref } from 'vue';

const open = ref(false);
const message = ref('');
const header = ref('Confirm');
let resolver: ((value: boolean) => void) | null = null;

function confirm(headerValue:string, msg: string) {
  header.value = headerValue;
  message.value = msg;
  open.value = true;

  return new Promise<boolean>((resolve) => {
    resolver = resolve;
  });
}

function onConfirm() {
  open.value = false;
  resolver?.(true);
}

function onCancel() {
  open.value = false;
  resolver?.(false);
}

defineExpose({ confirm });
</script>

<style scoped>
.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0,0,0,0.4);
  display: grid;
  place-items: center;
}
.dialog {
  background: white;
  padding: 20px;
  border-radius: 8px;
  min-width: 280px;
}
.actions {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}
</style>
