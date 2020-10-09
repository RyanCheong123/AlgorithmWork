#include<stdio.h>
#include<vector>
#include<algorithm>

using namespace std;

void selectionSort() {
	freopen("sorting.txt", "rt", stdin);
	int a[100], n, idx, i, j, tmp;

	scanf_s("%d", &n);
	for (i = 0; i < n; i++) {
		scanf_s("%d", &a[i]);
	}

	for (i = 0; i < n - 1; i++) {
		idx = i;
		for (j = i + 1; j < n; j++) {
			if (a[j] < a[idx]) {
				idx = j;
			}
		}
		tmp = a[i];
		a[i] = a[idx];
		a[idx] = tmp;
	}

	for (i = 0; i < n; i++) {
		printf("%d ", a[i]);
	}
	printf("\n");
}

void bubbleSort() {
	freopen("sorting.txt", "rt", stdin);
	int a[100], n, i, j, tmp;

	scanf_s("%d", &n);
	for (i = 0; i < n; i++) {
		scanf_s("%d", &a[i]);
	}

	for (i = 0; i < n - 1; i++) {
		for (j = 0; j < n - 1 - i; j++) {
			if (a[j] > a[j + 1]) {
				tmp = a[j];
				a[j] = a[j + 1];
				a[j + 1] = tmp;
			}
		}
	}

	for (i = 0; i < n; i++) {
		printf("%d ", a[i]);
	}
	printf("\n");
}

void insertionSort() {
	freopen("sorting.txt", "rt", stdin);
	int a[100], n, i, j, tmp;

	scanf_s("%d", &n);
	for (i = 0; i < n; i++) {
		scanf_s("%d", &a[i]);
	}

	for (i = 1; i < n; i++) {
		tmp = a[i];
		for (j = i - 1; j >= 0; j--) {
			if (a[j] > tmp) {
				a[j + 1] = a[j];
			}
			else {
				break;
			}
		}
		a[j + 1] = tmp;
	}

	for (i = 0; i < n; i++) {
		printf("%d ", a[i]);
	}
	printf("\n");
}

int main() {
	selectionSort();
	bubbleSort();
	insertionSort();
}

