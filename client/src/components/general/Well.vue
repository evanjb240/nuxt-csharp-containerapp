<template>
  <div v-if="show" :class="`${type}-well well`">
    <InfoIcon />
    <div>{{ message }}</div>
  </div>
</template>

<script setup lang="ts">
import { InfoIcon } from '@zhuowenli/vue-feather-icons'
import { WellType } from '~/types/Well'
import { watch, onBeforeUnmount } from 'vue'

interface Props {
  type: WellType
  message: string
  show: boolean
  timeout?: number
}

const props = withDefaults(defineProps<Props>(), {
  type: WellType.Error,
  message: '',
  show: false,
  timeout: 5000
})

const emit = defineEmits<{
  (e: 'update:show', value: boolean): void
}>()

let timer: ReturnType<typeof setTimeout> | null = null

watch(
  () => props.show,
  (newVal) => {
    if (newVal) {
      if (timer) clearTimeout(timer)

      timer = setTimeout(() => {
        emit('update:show', false)
      }, props.timeout)
    }
  }
)

onBeforeUnmount(() => {
  if (timer) clearTimeout(timer)
})
</script>

<style scoped>
.well {
  display: flex;
  gap: 5px;
  width: 100%;
  border-radius: 3px;
  align-items: center;
  padding: 10px;
  margin-top: 10px;
  box-sizing: border-box;
}
.error-well {
  background-color: red;
}
.success-well {
  background-color: lightgreen;
}
</style>
