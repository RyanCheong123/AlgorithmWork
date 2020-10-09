#include<stdio.h>
#include<vector>
#include<algorithm>

#pragma warning(disable:4996)

using namespace std;

void recur1(int x) {
	if (x == 0) return;
	recur1(x - 1);
	printf("%d ", x);
}

void toBinary(int x) {
	if (x == 0) return;
	toBinary(x / 2);
	printf("%d", x % 2);
}

void dfsPreO(int v) {
	if (v > 7) return;
	printf("%d", v);
	dfsPreO(v * 2);
	dfsPreO(v * 2 + 1);
}

void dfsInO(int v) {
	if (v > 7) return;
	dfsInO(v * 2);
	printf("%d", v);
	dfsInO(v * 2 + 1);
}

void dfsPostO(int v) {
	if (v > 7) return;
	dfsPostO(v * 2);
	dfsPostO(v * 2 + 1);
	printf("%d", v);
}

void mergeSort() {

}


int main() {
	//1.
	freopen("input.txt", "rt", stdin);
	int n;
	scanf_s("%d", &n);
	recur1(n);
	printf("\n");

	//2.
	freopen("toBinary.txt", "rt", stdin);
	scanf_s("%d", &n);
	toBinary(n);
	printf("\n");

	//3.
	dfsPreO(1);
	printf("\n");
	dfsInO(1);
	printf("\n");
	dfsPostO(1);
	printf("\n");

	//4

	return 0;
}